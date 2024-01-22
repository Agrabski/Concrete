import { Button, Stack } from "@mui/material";
import { IQuizTemplateQuestionReference, QuestionBank, QuizTemplate } from "../../api/Api";
import { ReplaceAtIndex } from "../../utils/ArrayUtils";
import { useState } from "react";
import { QuestionBankPicker } from "../QuestionBankPicker";

export interface QuizTemplateEditorProps {
	template: QuizTemplate;
	updateTemplate: (v: QuizTemplate) => void;
	questionBanks: { [key: string]: QuestionBank };
	updateAvailableBanks: (v: { [key: string]: QuestionBank }) => void;
	id: string | undefined;
}
interface QuizQuestionReferenceEditorProps {
	question: IQuizTemplateQuestionReference,
	updateQuestion: (q: IQuizTemplateQuestionReference) => void,
	questionBanks: { [key: string]: QuestionBank };
	updateAvailableBanks: (v: { [key: string]: QuestionBank }) => void;
	id: string | undefined;
}

function QuizQuestionReferenceEditor({ question, updateQuestion, questionBanks, updateAvailableBanks, id }: QuizQuestionReferenceEditorProps) {
	const [questionBankId, updateQuestionBankId] = useState<string | undefined>();
	const rest = () => {
		if (question.$type === 'single-question') {

		}
		if (question.$type === 'category') {

		}
		return <div >test</div>;
	};
	return <Stack direction='row' key={id}>
		<QuestionBankPicker
			availableBanks={questionBanks}
			updateAvailableBanks={updateAvailableBanks}
			selectedBankId={questionBankId}
			updateSelectedBankId={updateQuestionBankId}
		/>
		{rest()}
	</Stack>
}

export function QuizTemplateEditor({ template, updateTemplate, questionBanks, updateAvailableBanks, id }: QuizTemplateEditorProps) {
	return (
		<Stack key={id}>
			{template.questions.map((q, index) => <QuizQuestionReferenceEditor
				id={index.toString()}
				question={q}
				updateQuestion={n => updateTemplate({
					...template,
					questions: ReplaceAtIndex(template.questions, n, index)
				})}
				questionBanks={questionBanks}
				updateAvailableBanks={updateAvailableBanks}
			/>)
			}
			<Button onClick={() => updateTemplate({ ...template, questions: [...template.questions, { questionBankId: '', questionTemplateId: '', $type: 'single-question', filingMode: { $type: 'all' } }] })}>Add question</Button>
		</Stack>
	)
}
