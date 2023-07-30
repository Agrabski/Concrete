import { Typography } from "@mui/material";
import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { UserDto } from "../api/Api";
import { api } from "../api/ApiWrapper";
import { handleErrors } from "../api/ErrorHandling";

export function Home() {
	const [me, updateMe] = useState<UserDto | undefined>();
	const navigate = useNavigate();
	useEffect(() => {
		if (!me)
			api.api.userMeList().then(r => updateMe(r.data)).catch(handleErrors(navigate));
	});

	return <Typography>Concrete</Typography>
}