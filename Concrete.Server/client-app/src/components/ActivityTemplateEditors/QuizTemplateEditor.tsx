import { QuestionBank, QuizTemplate } from "../../api/Api";

export interface QuizTemplateEditorProps {
	template: QuizTemplate;
	updateTemplate: (v: QuizTemplate) => void;
	questionBanks: { [key: string]: QuestionBank };
}

export function QuizTemplateEditor({ template, updateTemplate }: QuizTemplateEditorProps) {

}
