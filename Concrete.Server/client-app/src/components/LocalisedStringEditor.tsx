import { LocalisedString } from "../api/Api";

export interface LocalisedStringEditorProps {
	value: LocalisedString | undefined,
	valueChanged: (value: LocalisedString) => void
}

export function LocalisedStringEditor({ }: LocalisedStringEditorProps) {
	return <div />;
}