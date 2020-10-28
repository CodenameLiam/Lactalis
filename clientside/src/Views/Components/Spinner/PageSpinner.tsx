import React from "react";
import { PuffLoader } from "react-spinners";

export default function PageSpinner() {
	return (
		<div className="page-spinner">
			<PuffLoader size={150} color="#71d2f5" />
		</div>
	);
}
