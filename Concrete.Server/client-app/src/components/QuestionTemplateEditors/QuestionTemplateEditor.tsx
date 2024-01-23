import { Typography } from "@mui/material";
import { IQuestionTemplate, QuizTemplate } from "../../api/Api";



interface QuestionTemplateEditorProps {
	question: IQuestionTemplate;
	updateQuestion: (v: IQuestionTemplate) => void;
	id: string | undefined
}

export function QuestionTemplateEditor({ question, updateQuestion, id }: QuestionTemplateEditorProps): JSX.Element {
	return <div />;
}
