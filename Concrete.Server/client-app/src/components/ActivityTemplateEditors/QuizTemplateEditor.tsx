import { Stack } from "@mui/material";
import { IQuizTemplateQuestionReference, QuestionBank, QuizTemplate } from "../../api/Api";
import { ReplaceAtIndex } from "../../utils/ArrayUtils";
import { useState } from "react";
import { QuestionBankPicker } from "../QuestionBankPicker";

export interface QuizTemplateEditorProps {
	template: QuizTemplate;
	updateTemplate: (v: QuizTemplate) => void;
	questionBanks: { [key: string]: QuestionBank };
	updateAvailableBanks: (v: { [key: string]: QuestionBank }) => void;
}
interface QuizQuestionReferenceEditorProps {
	question: IQuizTemplateQuestionReference,
	updateQuestion: (q: IQuizTemplateQuestionReference) => void,
	questionBanks: { [key: string]: QuestionBank };
	updateAvailableBanks: (v: { [key: string]: QuestionBank }) => void;
}

function QuizQuestionReferenceEditor({ question, updateQuestion, questionBanks, updateAvailableBanks }: QuizQuestionReferenceEditorProps) {
	const [questionBankId, updateQuestionBankId] = useState<string | undefined>();
	const rest = () => {
		if (question.$type === 'single-question') {

		}
		if (question.$type === 'category') {

		}
		return <div />;
	};
	return <Stack direction='row'>
		<QuestionBankPicker
			availableBanks={questionBanks}
			updateAvailableBanks={updateAvailableBanks}
			selectedBankId={questionBankId}
			updateSelectedBankId={updateQuestionBankId}
		/>
		{rest()}
	</Stack>
}

export function QuizTemplateEditor({ template, updateTemplate, questionBanks, updateAvailableBanks }: QuizTemplateEditorProps) {
	return (
		<Stack>
			{template.questions.map((q, index) => <QuizQuestionReferenceEditor
				question={q}
				updateQuestion={n => updateTemplate({
					...template,
					questions: ReplaceAtIndex(template.questions, n, index)
				})}
				questionBanks={questionBanks}
				updateAvailableBanks={updateAvailableBanks}
			/>)
			}
		</Stack>
	)
}
