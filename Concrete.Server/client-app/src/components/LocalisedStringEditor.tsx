import { Accordion, AccordionDetails, AccordionSummary, Box, IconButton, Stack, TextField } from "@mui/material";
import { LocalisedString } from "../api/Api";
import AddIcon from '@mui/icons-material/Add';

export interface LocalisedStringEditorProps {
	name: string,
	value: LocalisedString,
	valueChanged: (value: LocalisedString) => void;
	id: string | undefined;
}

export function LocalisedStringEditor({ name, value, valueChanged, id }: LocalisedStringEditorProps) {
	return (
		<Accordion key={id}>
			<AccordionSummary>{name}</AccordionSummary>
			<AccordionDetails>
				{Object.keys(value.textByLocale).map(k => (
					<Box key={k || 'empty'}>
						<Stack direction='row'>
							<TextField id='locale' label='Locale' sx={{ width: '20%' }} value={k} onChange={e => {
								const nv: Record<string, string> = {};
								for (const key in value.textByLocale) {
									if (key !== k) {
										nv[key] = value.textByLocale[key];
									}
								}
								nv[e.target.value!] = value.textByLocale[k];
								valueChanged({ textByLocale: nv })
							}}
							/>
							<TextField id='value' sx={{ width: '80%' }} label='Text' value={value.textByLocale[k]} onChange={e => {
								valueChanged({ textByLocale: { ...value.textByLocale, [k]: e.target.value } });
							}} />
						</Stack>
					</Box>
				))
				}
				<IconButton id='add' onClick={() => valueChanged({ textByLocale: { ...value.textByLocale, ['']: '' } })}><AddIcon />Add locale</IconButton>
			</AccordionDetails>
		</Accordion >
	);
}