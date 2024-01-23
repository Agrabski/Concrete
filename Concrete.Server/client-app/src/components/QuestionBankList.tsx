import { CircularProgress, IconButton, List, Stack, Typography } from "@mui/material";
import { useCallback, useEffect, useState } from "react";
import { CourseHeader, QuestionBank, QuestionBankHeader } from "../api/Api";
import { api } from "../api/ApiWrapper";
import AddIcon from '@mui/icons-material/Add';
import { DataGrid, GridColDef } from '@mui/x-data-grid';
import { useNavigate } from "react-router-dom";
// todo: more properties
const columns: GridColDef<CourseHeader>[] = [
    {
        valueGetter: h => h.row.name,
        type: 'string',
        field: 'Name'
    }
]

export function QuestionBanks() {
    const [questionBanks, updateQuestionBanks] = useState<QuestionBankHeader[] | undefined>();
    let navigate = useNavigate();
    useEffect(() => {
        if (!questionBanks) {
            api.api.questionBankList().then(r => updateQuestionBanks(r.data));
        }
    })
    const createBank = useCallback(async () => {
        const id = crypto.randomUUID()
        await api.api.questionBankCreateCreate({
            name: 'New bank',
            questionTemplates: [],
            id,
            categories: []
        });
        navigate(`/question-banks/${id}`);
    }, [navigate]);
    return (
        <Stack sx={{ height: '80vh' }} >
            <Stack direction='row'>
                <IconButton onClick={createBank}>
                    <AddIcon />
                    <Typography>Add question bank</Typography>
                </IconButton>
            </Stack>
            {questionBanks ? (<DataGrid rows={questionBanks} columns={columns} />) : <CircularProgress />}
        </Stack >
    );
}