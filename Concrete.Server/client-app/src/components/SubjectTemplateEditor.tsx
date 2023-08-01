import { Stack, TextField } from "@mui/material";
import { SubjectTemplate } from "../api/Api";
import { LocalisedStringEditor } from "./LocalisedStringEditor";

export interface SubjectTemplateEditorProps {
	value: SubjectTemplate,
	updateValue: (v: SubjectTemplate) => void
}
export function SubjectTemplateEditor({ value, updateValue }: SubjectTemplateEditorProps) {
	return (
		<Stack sx={{ margin: 2 }}>
			<TextField label='Id' disabled value={value.id} />
			<LocalisedStringEditor name='Name' value={value.name} valueChanged={v => updateValue({ ...value, name: v })} />
			<LocalisedStringEditor name='Description' value={value.description} valueChanged={v => updateValue({ ...value, description: v })} />
		</Stack>
	);
}