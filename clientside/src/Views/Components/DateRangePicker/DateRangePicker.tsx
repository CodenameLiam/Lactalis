import * as React from "react";
import { observer } from "mobx-react";
import { DateTimePicker, IDateTimePickerProps } from "../DateTimePicker/DateTimePicker";

/** DateRangePicker-specific properties. Extend as necessary. */
export interface IDateRangePickerProps<T> extends IDateTimePickerProps<T> {}

/**
 * DateRangePicker Component. Wraps DateTimePicker, which in turn wraps Flatpickr.
 * See IDateTimePickerProps for root property definitions. Can pass Flatpickr
 * properties that are not implemented by this interface via this.flatpickrProps.
 */
@observer
export class DateRangePicker<T> extends React.Component<IDateRangePickerProps<T>> {
	public render() {
		return (
			<DateTimePicker
				/* The two options below are only applied if the humanReadable
				 * property is set to true on Component instantiation. */
				altFormat="j F, Y"
				dateFormat="Y-m-d"
				/* Set the Flatpickr to allow selection of a date range. */
				mode="range"
				enableTime={false}
				{...this.props}
			/>
		);
	}
}
