import * as React from "react";
import { ToastOptions, toast, TypeOptions } from "react-toastify";
import classNames from "classnames";
import "react-toastify/dist/ReactToastify.css";

/**
 * Show alert as a toast
 * @param errorMsg the text to be printed on the alert toast
 * @param type The type of the toast
 * @param options additional options such as 'autoClose' | 'position'
 */
export default function alert(
	errorMsg: React.ReactNode,
	type: TypeOptions = "info",
	options: ToastOptions = {}
) {
	toast(<p>{errorMsg}</p>, {
		className: classNames("alert", "alert__" + type),
		type,
		...options,
	});
}
