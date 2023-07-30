import React, { useEffect } from 'react';
import { AppBar, Divider, Drawer, IconButton, List, ListItem, Stack, Toolbar, Typography } from "@mui/material"
import { useState } from "react"
import MenuIcon from '@mui/icons-material/Menu';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import { Routes, Route, useNavigate } from 'react-router-dom';
import { Login } from './Login';
import { Home } from './Home';
import { UserDto } from '../api/Api';
import { api } from '../api/ApiWrapper';
import HomeIcon from '@mui/icons-material/Home';
import { Courses } from './Courses';

export interface ConcreteAppRoute {
	icon: string,
	route: string
}

export interface ConcreteAppWrapperParams {
}

export function ConcreteAppWrapper({ }: ConcreteAppWrapperParams): JSX.Element {
	const [drawerOpen, updateDrawerOpen] = useState(false);
	const n = useNavigate();
	const navigate = (url: string) => {
		n(url);
		updateDrawerOpen(false);
	}

	return (
		<Stack>
			<AppBar position="sticky">
				<Toolbar>
					<IconButton onClick={() => updateDrawerOpen(true)}><MenuIcon /></IconButton>
				</Toolbar>
				<Drawer
					open={drawerOpen}
					onClose={() => updateDrawerOpen(false)}>
					<List>
						<ListItem>
							<IconButton onClick={() => updateDrawerOpen(false)}>
								<ArrowBackIcon />
								<Typography align='right'>Concrete</Typography>
							</IconButton>
						</ListItem>
						<ListItem>
							<IconButton onClick={() => navigate('/')}>
								<HomeIcon />
								<Typography>Dashboard</Typography>
							</IconButton>
						</ListItem>

						<Divider />

					</List>
				</Drawer>
			</AppBar>
			<Routes>
				<Route path='login' Component={Login} />
				<Route path='courses' Component={Courses} />
				<Route path='/' Component={Home} />
			</Routes>
		</Stack>
	)
}