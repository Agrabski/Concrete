import { useParams } from "react-router-dom";
import { QuestionBank } from "../api/Api";
import { useEffect, useState } from "react";
import { api } from "../api/ApiWrapper";
import { CircularProgress, Stack, TextField } from "@mui/material";
import { CategoryNameEditor } from "./CategoryEditor";
import { QuestionTemplateEditor } from "./QuestionTemplateEditors/QuestionTemplateEditor";


export function QuestionBankEdit() {
    const { bankID } = useParams();
    const [bank, updateBank] = useState<QuestionBank | undefined>();
    useEffect(() => {
        if (!bank) {
            api.api.questionBankDetail(bankID!).then(r => updateBank(r.data));
        }
    })
    if (!bank) {
        return <CircularProgress />
    }
    return (
        <Stack>
            <TextField name="name" label="Name" value={bank.name} onChange={e => updateBank({ ...bank, name: e.target.value })} />
            <Stack>
                {bank.categories.map(category => <CategoryNameEditor categoryName={category} />)}
            </Stack>
            <Stack>
                {bank.questionTemplates.map(question => <QuestionTemplateEditor
                    question={question}
                    updateQuestion={v => updateBank({ ...bank, questionTemplates: bank.questionTemplates.map(q => q.id === question.id ? v : q) })}
                    id={question.id}
                />)
                }
            </Stack>
        </Stack>
    )
}