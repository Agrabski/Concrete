import { useCallback, useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { CourseTemplate } from "../api/Api";
import { api } from "../api/ApiWrapper";
import { Box, Button, Card, CardActions, CardContent, CircularProgress, FormControlLabel, IconButton, Stack, Switch, TextField, Typography, debounce } from "@mui/material";
import { SubjectTemplateEditor } from "./SubjectTemplateEditor";
import { RemoveAtIndex, ReplaceAtIndex } from "../utils/ArrayUtils";
import AddIcon from '@mui/icons-material/Add';
import JsonEditor from 'react-json-editor-ui'

interface EditorProps {
	template: CourseTemplate,
	updateTemplate: (template: CourseTemplate) => void
}
function EditByJson({ template, updateTemplate }: EditorProps) {
	// todo: actually good json editor. ffs imma use monaco at some point i fucking swear
	return <JsonEditor data={template} onChange={updateTemplate} />
}
function EditByEditor({ template, updateTemplate }: EditorProps) {
	return (
		<Stack>
			<Typography>Subjects</Typography>
			<Box sx={{ marign: 2 }}>
				<Stack spacing={2}>
					{template.subjects.map((s, index) => {
						return <Card>
							<SubjectTemplateEditor
								value={s}
								updateValue={v => updateTemplate({
									...template,
									subjects: ReplaceAtIndex(template.subjects, v, index)
								})}
							/>
							<CardActions>
								<Button onClick={() => updateTemplate({
									...template,
									subjects: RemoveAtIndex(template.subjects, index)
								})}>Remove</Button>
							</CardActions>
						</Card>
					})}
					<IconButton onClick={() => updateTemplate({
						...template,
						subjects: [
							...template.subjects,
							{
								activities: [],
								name: { textByLocale: {} },
								description: { textByLocale: {} },
								id: crypto.randomUUID(),
								tempalteName: 'Untitled subject template'
							}
						]
					})}>
						<AddIcon />Add
					</IconButton>
				</Stack>
			</Box>
		</Stack>
	);
}

export function CourseTemplateEdit() {
	const [showJson, updateShowJson] = useState(false);
	const { templateID } = useParams();
	const [template, updateTemplate] = useState<CourseTemplate | undefined>();
	const debouncedApiCallback = useCallback(debounce((v: CourseTemplate) => {
		api.api.courseTemplateCreate(v);
	}, 5_000), []);
	const updateTemplateInApi = useCallback((v: CourseTemplate) => {
		updateTemplate(v);
		debouncedApiCallback(v);
	}, []);
	useEffect(() => {
		if (!template) {
			api.api.courseTemplateDetail(templateID!).then(r => {
				updateTemplate(r.data);
			})
		}
	})
	if (!template) {
		// todo
		return <CircularProgress />;
	}
	return (
		<Stack sx={{ padding: '2rem' }}>
			<TextField label='Name' value={template.templateName} onChange={e => updateTemplateInApi({ ...template, templateName: e.target.value! })} />
			<FormControlLabel control={<Switch checked={showJson} onChange={c => updateShowJson(c.target.checked)} />} label='Show json' />
			{
				showJson ?
					<EditByJson template={template} updateTemplate={updateTemplateInApi} /> :
					<EditByEditor template={template} updateTemplate={updateTemplateInApi} />
			}
		</Stack>
	)
}