import { Select } from "@mui/material";
import { QuestionBank } from "../api/Api";

export interface QuestionBankPickerProps {
	selectedBankId?: string,
	availableBanks: { [key: string]: QuestionBank },
	updateSelectedBankId: (id: string | undefined) => void,
	updateAvailableBanks: (v: { [key: string]: QuestionBank }) => void
}

export function QuestionBankPicker({ selectedBankId, updateSelectedBankId, availableBanks }: QuestionBankPickerProps) {
	return <Select value={selectedBankId} onChange={v => updateSelectedBankId(v.target.value)}>
	</Select>
}
