export function ReplaceAtIndex<T>(array: T[], value: T, index: number) {
	return [...array.slice(0, index), value, ...array.slice(index + 1)];
}

export function RemoveAtIndex<T>(array: T[], index: number) {
	return [...array.slice(0, index), ...array.slice(index + 1)];
}
