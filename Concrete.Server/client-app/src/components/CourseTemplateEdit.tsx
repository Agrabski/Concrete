import { useCallback, useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { CourseTemplate } from "../api/Api";
import { api } from "../api/ApiWrapper";
import { Box, Button, Card, CardActions, CardContent, CircularProgress, FormControlLabel, IconButton, Stack, Switch, TextField, debounce } from "@mui/material";
import { LocalisedStringEditor } from "./LocalisedStringEditor";
import { SubjectTemplateEditor } from "./SubjectTemplateEditor";
import { RemoveAtIndex, ReplaceAtIndex } from "../utils/ArrayUtils";
import AddIcon from '@mui/icons-material/Add';

interface EditorProps {
	template: CourseTemplate,
	updateTemplate: (template: CourseTemplate) => void
}
function EditByJson({ }: EditorProps) {
	return <div >json</div>;
}
function EditByEditor({ template, updateTemplate }: EditorProps) {
	return (
		<Card>
			<CardContent>Subjects</CardContent>
			<Stack spacing={2}>
				{template.subjects.map((s, index) => {
					return <Card sx={{ margin: 1 }}>
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
							id: crypto.randomUUID()
						}
					]
				})}>
					<AddIcon />Add
				</IconButton>
			</Stack>
		</Card>
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