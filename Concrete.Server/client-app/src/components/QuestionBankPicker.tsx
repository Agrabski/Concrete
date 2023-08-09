import { Button, MenuItem, Select } from "@mui/material";
import { QuestionBank } from "../api/Api";

export interface QuestionBankPickerProps {
	selectedBankId?: string,
	availableBanks: { [key: string]: QuestionBank },
	updateSelectedBankId: (id: string | undefined) => void,
	updateAvailableBanks: (v: { [key: string]: QuestionBank }) => void
}

export function QuestionBankPicker({ selectedBankId, updateSelectedBankId, availableBanks }: QuestionBankPickerProps) {
	const bankIds = Object.keys(availableBanks);
	return <Select value={selectedBankId} onChange={v => updateSelectedBankId(v.target.value)}>
		{bankIds.map(id => <MenuItem key={id} value={id} >{availableBanks[id].name}</MenuItem>)}
		<Button>Import question bank</Button>
	</Select>
}
