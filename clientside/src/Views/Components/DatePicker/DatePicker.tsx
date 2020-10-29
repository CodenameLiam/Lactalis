import * as React from "react";
import { observer } from "mobx-react";
import { DateTimePicker, IDateTimePickerProps } from "../DateTimePicker/DateTimePicker";

/** DatePicker-specific properties. Extend as necessary. */
export interface IDatePickerProps<T> extends IDateTimePickerProps<T> {}

/**
 * DatePicker Component. Wraps DateTimePicker, which in turn wraps Flatpickr.
 * See IDateTimePickerProps for root property definitions. Can pass Flatpickr
 * properties that are not implemented by this interface via this.flatpickrProps.
 */
@observer
export class DatePicker<T> extends React.Component<IDatePickerProps<T>> {
	public render() {
		return (
			<DateTimePicker
				/* The two options below are only applied if the humanReadable
				 * property is set to true on Component instantiation. */
				altFormat="j F, Y"
				dateFormat="Y-m-d"
				/* Set the Flatpickr to allow selection of dates only. */
				enableTime={false}
				{...this.props}
			/>
		);
	}
}
