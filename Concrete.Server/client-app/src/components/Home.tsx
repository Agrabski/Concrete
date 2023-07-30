import { Typography } from "@mui/material";
import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { UserDto } from "../api/Api";
import { api } from "../api/ApiWrapper";

export function Home() {
	const [me, updateMe] = useState<UserDto | undefined>();
	const navigate = useNavigate();
	useEffect(() => {
		if (!me)
			api.api.userMeList().then(r => updateMe(r.data)).catch(r => {
				if ((r as Response).status === 401) {
					navigate('login');
				}
			});
	});

	return <Typography>Concrete</Typography>
}