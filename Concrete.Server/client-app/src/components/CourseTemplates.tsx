import { CircularProgress, IconButton, List, Stack, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import { CourseHeader, CourseTemplateHeader } from "../api/Api";
import { api } from "../api/ApiWrapper";
import { DataGrid, GridColDef } from '@mui/x-data-grid';
import AddIcon from '@mui/icons-material/Add';
import { NavigateFunction, useNavigate } from "react-router-dom";
// todo: more properties
const columns: GridColDef<CourseHeader>[] = [
	{
		valueGetter: h => h.row.name,
		type: 'string',
		field: 'Name',
		width: 600
	}
]

function createTemplate(navigate: NavigateFunction) {
	return () => {
		api.api.courseTemplateCreateCreate({ name: 'Untitled course' }).then(r => {
			navigate(`/course-templates/${r.data.templateId}`);
		});
	}
}

export function CourseTemplates() {
	const [courseTemplates, updateCourseTemplates] = useState<CourseTemplateHeader[] | undefined>();
	const navigate = useNavigate();
	useEffect(() => {
		if (!courseTemplates) {
			api.api.courseTemplateList().then(r => updateCourseTemplates(r.data));
		}
	})

	return (
		<Stack sx={{ height: '80vh' }} >
			<Stack direction='row'>
				<IconButton onClick={createTemplate(navigate)}>
					<AddIcon />
					<Typography>Add template</Typography>
				</IconButton>
			</Stack>
			{courseTemplates ? (
				<DataGrid
					rows={courseTemplates}
					columns={columns}
					onRowDoubleClick={r => navigate(`/course-templates/${r.id}`)} />)
				: <CircularProgress />
			}
		</Stack >
	);
}