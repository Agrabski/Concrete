import React from "react";
import { NavigateFunction } from "react-router-dom";

export function handleErrors(navigate: NavigateFunction) {
	return (r: any) => {
		if ((r as Response).status === 401) {
			navigate('/login');
		}
	}
}