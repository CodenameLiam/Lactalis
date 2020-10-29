import React from "react";
import { PuffLoader } from "react-spinners";

export default function ComponentSpinner() {
	return (
		<div className="component-spinner">
			<PuffLoader size={150} color="#71d2f5" />
		</div>
	);
}
