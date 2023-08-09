import { Typography } from "@mui/material";
import { QuestionBank, QuizTemplate, SubjectActivity } from "../../api/Api";
import { QuizTemplateEditor } from "./QuizTemplateEditor";



interface ActivityTemplateEditorProps {
	actvity: SubjectActivity;
	updateActivities: (v: SubjectActivity) => void;
	questionBanks: { [key: string]: QuestionBank };
	updateAvailableBanks: (v: { [key: string]: QuestionBank }) => void;
	id: string | undefined
}

export function ActivityTemplateEditor({ actvity, updateActivities, questionBanks, updateAvailableBanks, id }: ActivityTemplateEditorProps): JSX.Element {
	if (actvity.template.$$type === 'Concrete::Core::Quiz') {
		return <QuizTemplateEditor
			id={id}
			template={actvity.template as QuizTemplate}
			updateTemplate={v => updateActivities({ ...actvity, template: { ...v, $$type: 'Concrete::Core::Quiz' } })}
			questionBanks={questionBanks}
			updateAvailableBanks={updateAvailableBanks}
		/>
	}
	return <Typography>Unrecognised activity type {actvity.template.$$type}</Typography>
}
