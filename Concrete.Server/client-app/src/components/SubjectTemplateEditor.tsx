import { Accordion, AccordionDetails, AccordionSummary, IconButton, Stack, TextField } from "@mui/material";
import { SubjectActivity, SubjectTemplate } from "../api/Api";
import { LocalisedStringEditor } from "./LocalisedStringEditor";
import { ReplaceAtIndex } from "../utils/ArrayUtils";
import AddIcon from '@mui/icons-material/Add';

export interface SubjectTemplateEditorProps {
	value: SubjectTemplate,
	updateValue: (v: SubjectTemplate) => void
}

function ActivityTemplateEditor({ actvity, updateActivities }: { actvity: SubjectActivity, updateActivities: (v: SubjectActivity) => void }): JSX.Element {
	return <div />;
}

export function SubjectTemplateEditor({ value, updateValue }: SubjectTemplateEditorProps) {
	return (
		<Stack sx={{ margin: 2 }}>
			<TextField label='Template name' value={value.tempalteName} />
			<LocalisedStringEditor name='Display name' value={value.name} valueChanged={v => updateValue({ ...value, name: v })} />
			<LocalisedStringEditor name='Description' value={value.description} valueChanged={v => updateValue({ ...value, description: v })} />
			<Accordion>
				<AccordionSummary>Activities</AccordionSummary>
				<AccordionDetails>
					{value.activities.map((a, index) => <ActivityTemplateEditor
						actvity={a}
						updateActivities={v => updateValue({
							...value,
							activities: ReplaceAtIndex(value.activities, v, index)
						})}
					/>)}
					<IconButton><AddIcon /> Add activity</IconButton>
				</AccordionDetails>
			</Accordion>
		</Stack>
	);
}
