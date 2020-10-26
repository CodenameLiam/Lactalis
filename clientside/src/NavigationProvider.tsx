import React, { createContext, useState } from "react";

export const AppContext = createContext<any>(undefined);

export default function NavigationProvider(props: { children: any }) {
	const [appState, setAppState] = useState({
		navOpen: true,
		navVisible: true,
	});

	return (
		<AppContext.Provider value={{ appState, setAppState }}>{props.children}</AppContext.Provider>
	);
}
