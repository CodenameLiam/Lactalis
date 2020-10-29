import * as React from "react";
import { observer } from "mobx-react";
import "flatpickr/dist/themes/material_green.css";
import { DateTimePicker, IDateTimePickerProps } from "../DateTimePicker/DateTimePicker";

/** TimePicker-specific properties. Extend as necessary. */
export interface ITimePickerProps<T> extends IDateTimePickerProps<T> {}

/**
 * TimePicker Component. Wraps DateTimePicker, which in turn wraps Flatpickr.
 * See IDateTimePickerProps for root property definitions. Can pass Flatpickr
 * properties that are not implemented by this interface via this.flatpickrProps.
 */
@observer
export class TimePicker<T> extends React.Component<ITimePickerProps<T>> {
	public render() {
		return (
			<DateTimePicker
				/* The two options below are only applied if the humanReadable
				 * property is set to true on Component instantiation.
				 * Displays the time in 12hr format. Default is 24hr. */
				altFormat="h:i K"
				dateFormat="H:i"
				/* Set the Flatpickr to allow selection of a single time only. */
				mode="time"
				noCalendar={true}
				{...this.props}
			/>
		);
	}
}
