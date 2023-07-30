import { CircularProgress, List, Stack, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import { CourseHeader } from "../api/Api";
import { api } from "../api/ApiWrapper";
import { DataGrid, GridColDef } from '@mui/x-data-grid';
// todo: more properties
const columns: GridColDef<CourseHeader>[] = [
	{
		valueGetter: h => h.row.name,
		type: 'string',
		field: 'Name'
	}
]

export function Courses() {
	const [courses, updateCourses] = useState<CourseHeader[] | undefined>();
	useEffect(() => {
		if (!courses) {
			api.api.courseList().then(r => updateCourses(r.data));
		}
	})
	return (
		<Stack sx={{ height: '80vh' }} >
			{courses ? (<DataGrid rows={courses} columns={columns} />) : <CircularProgress />}
		</Stack >
	);
}