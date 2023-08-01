import { Accordion, AccordionDetails, AccordionSummary, Box, IconButton, Stack, TextField } from "@mui/material";
import { LocalisedString } from "../api/Api";
import AddIcon from '@mui/icons-material/Add';

export interface LocalisedStringEditorProps {
	name: string,
	value: LocalisedString,
	valueChanged: (value: LocalisedString) => void
}

export function LocalisedStringEditor({ name, value, valueChanged }: LocalisedStringEditorProps) {
	return (
		<Accordion>
			<AccordionSummary>{name}</AccordionSummary>
			<AccordionDetails>
				<Stack>
					{Object.keys(value.textByLocale).map(k => (
						<Box>
							<Stack direction='row'>
								<TextField label='Locale' sx={{ width: '20%' }} value={k} onChange={e => {
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
								<TextField sx={{ width: '80%' }} label='Text' value={value.textByLocale[k]} onChange={e => {
									valueChanged({ textByLocale: { ...value.textByLocale, [k]: e.target.value } });
								}} />
							</Stack>
						</Box>
					))
					}
					<IconButton onClick={() => valueChanged({ textByLocale: { ...value.textByLocale, ['']: '' } })}><AddIcon />Add locale</IconButton>
				</Stack>
			</AccordionDetails>
		</Accordion >
	);
}