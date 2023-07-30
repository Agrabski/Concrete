import { Button, Stack, TextField, Typography } from "@mui/material";
import { useState } from "react";
import { api } from "../api/ApiWrapper";
import { useNavigate, useParams } from "react-router-dom";

export function Login(): JSX.Element {
	const [username, updateUsername] = useState('');
	const [password, updatePassword] = useState('');
	const params = useParams();
	const nav = useNavigate();
	return (
		<Stack>
			<TextField
				required
				value={username}
				onChange={v => updateUsername(v.target.value)}
				label='Username'
			/>
			<TextField
				required
				type="password"
				autoComplete="current-password"
				value={password}
				onChange={v => updatePassword(v.target.value)}
				label='Password'
			/>
			<Button onClick={async () => {
				await api.api.userAuthCreate({ username, password });
				nav(params['returnUrl'] ?? "/");
			}}>
				<Typography>Login</Typography>
			</Button>
		</Stack>
	);
}
