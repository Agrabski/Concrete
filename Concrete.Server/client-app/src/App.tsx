import React from 'react';
import './App.css';
import { BrowserRouter, Route, Router, Routes } from 'react-router-dom';
import { ConcreteAppWrapper } from './components/ConcreteAppWrapper';
import { Login } from './components/Login';
import { ThemeProvider } from '@emotion/react';
import { createTheme } from '@mui/material';

const theme = createTheme({
	components: {
		MuiTextField: {
			styleOverrides: {
				root: {
					margin: '1rem'
				}

			}
		}
	}
});

function App() {
	return (
		<ThemeProvider theme={theme}>

			<BrowserRouter>
				<ConcreteAppWrapper />
			</BrowserRouter>
		</ThemeProvider>
	);
}

export default App;
