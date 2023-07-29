import { Button, Stack, TextField, Typography } from "@mui/material";
import { useState } from "react";
import { api } from "../api/ApiWrapper";

export function Login(): JSX.Element {
	const [username, updateUsername] = useState('');
	const [password, updatePassword] = useState('');
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
			<Button onClick={() => api.api.userAuthCreate({ username, password })}>
				<Typography>Login</Typography>
			</Button>
		</Stack>
	);
}
