import React, { useEffect } from 'react';
import { AppBar, Divider, Drawer, IconButton, List, ListItem, Stack, Toolbar, Typography } from "@mui/material"
import { useState } from "react"
import MenuIcon from '@mui/icons-material/Menu';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import { Routes, Route, useNavigate, useRoutes, useLocation } from 'react-router-dom';
import { Login } from './Login';
import { Home } from './Home';
import { UserDto } from '../api/Api';
import { api } from '../api/ApiWrapper';
import HomeIcon from '@mui/icons-material/Home';
import { Courses } from './Courses';
import { CourseTemplates } from './CourseTemplates';
import SourceIcon from '@mui/icons-material/Source';
import { CourseTemplateEdit } from './CourseTemplateEdit';
import { QuestionBankEdit } from './QuestionBankEdit';
import { QuestionBanks } from './QuestionBankList';

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
	const location = useLocation();

	function pickTextForLocation(pathname: string): React.ReactNode {
		switch (pathname) {
			case 'login': return 'Login';
			case '/courses': return 'Courses';
		}
	}

	return (
		<Stack>
			<AppBar position="sticky">
				<Toolbar>
					<IconButton onClick={() => updateDrawerOpen(true)}><MenuIcon /></IconButton>
					<Typography>{pickTextForLocation(location.pathname)}</Typography>
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

						<ListItem>
							<IconButton onClick={() => navigate('/courses')}>
								<HomeIcon />
								<Typography>Courses</Typography>
							</IconButton>
						</ListItem>
						<ListItem>
							<IconButton onClick={() => navigate('/course-templates')}>
								<SourceIcon />
								<Typography>Course templates</Typography>
							</IconButton>
						</ListItem>
					</List>
				</Drawer>
			</AppBar>
			<Routes>
				<Route path='login' Component={Login} />
				<Route path='courses' Component={Courses} />
				<Route path='course-templates' Component={CourseTemplates} />
				<Route path='course-templates/:templateID' Component={CourseTemplateEdit} />
				<Route path='question-banks' Component={QuestionBanks} />
				<Route path='question-banks/:bankID' Component={QuestionBankEdit} />
				<Route path='/' Component={Home} />
			</Routes>
		</Stack>
	)
}