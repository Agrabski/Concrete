import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { CourseTemplate } from "../api/Api";
import { api } from "../api/ApiWrapper";
import { Box, FormControlLabel, Stack, Switch, TextField } from "@mui/material";
import { LocalisedStringEditor } from "./LocalisedStringEditor";
interface EditorProps {
	template: CourseTemplate
}
function EditByJson() {
	return <div >json</div>;
}
function EditByEditor({ template }: EditorProps) {
	return (
		<Stack>
			{template.subjects!.map(s => {
				return <Box>
					<Stack>
						<LocalisedStringEditor value={s.name} valueChanged={() => { }}/>
					</Stack>
				</Box>
			})}
		</Stack>
	)
}

export function CourseTemplateEdit() {
	const [showJson, updateShowJson] = useState(false);
	const { templateID } = useParams();
	const [template, updateTemplate] = useState<CourseTemplate | undefined>();
	useEffect(() => {
		if (!template) {
			api.api.courseTemplateDetail(templateID!).then(r => {
				updateTemplate(r.data);
			})
		}
	})
	return (
		<Stack sx={{ padding: '2rem' }}>
			<TextField label='Name' value={template?.templateName} onChange={e => updateTemplate({ ...template, templateName: e.target.value })} />
			<FormControlLabel control={<Switch checked={showJson} onChange={c => updateShowJson(c.target.checked)} />} label='Show json' />
			{
				showJson ?
					<EditByJson /> :
					<EditByEditor template={template!} />
			}
		</Stack>
	)
}