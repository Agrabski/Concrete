import { Accordion, AccordionDetails, AccordionSummary, IconButton, Stack, TextField } from "@mui/material";
import { QuestionBank, SubjectTemplate } from "../api/Api";
import { LocalisedStringEditor } from "./LocalisedStringEditor";
import { ReplaceAtIndex } from "../utils/ArrayUtils";
import AddIcon from '@mui/icons-material/Add';
import { ActivityTemplateEditor } from "./ActivityTemplateEditors/ActivityTemplateEditor";

export interface SubjectTemplateEditorProps {
	value: SubjectTemplate;
	updateValue: (v: SubjectTemplate) => void;
	questionBanks: { [key: string]: QuestionBank };
	updateAvailableBanks: (v: { [key: string]: QuestionBank }) => void;

}

export function SubjectTemplateEditor({ value, updateValue, questionBanks, updateAvailableBanks }: SubjectTemplateEditorProps) {
	return (
		<Stack sx={{ margin: 2 }}>
			<TextField key='name' label='Template name' value={value.tempalteName} />
			<LocalisedStringEditor id='name-localised' name='Display name' value={value.name} valueChanged={v => updateValue({ ...value, name: v })} />
			<LocalisedStringEditor id='description' name='Description' value={value.description} valueChanged={v => updateValue({ ...value, description: v })} />
			<Accordion>
				<AccordionSummary>Activities</AccordionSummary>
				<AccordionDetails>
					{value.activities.map((a, index) => <ActivityTemplateEditor id={a.id}
						actvity={a}
						updateAvailableBanks={updateAvailableBanks}
						questionBanks={questionBanks}
						updateActivities={v => updateValue({
							...value,
							activities: ReplaceAtIndex(value.activities, v, index)
						})}
					/>)}
					<IconButton key='add' onClick={() => updateValue({
						...value,
						activities: [...value.activities, {
							id: crypto.randomUUID(),
							template: {
								$$type: 'Concrete::Core::Quiz',
								questions: []
							}
						}]
					})}><AddIcon /> Add activity</IconButton>
				</AccordionDetails>
			</Accordion>
		</Stack>
	);
}
