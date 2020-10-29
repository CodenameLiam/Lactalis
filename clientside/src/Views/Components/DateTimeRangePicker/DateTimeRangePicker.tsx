import * as React from "react";
import { observer } from "mobx-react";
import { DateTimePicker, IDateTimePickerProps } from "../DateTimePicker/DateTimePicker";

/** DateTimeRangePicker-specific properties. Extend as necessary. */
export interface IDateTimeRangePickerProps<T> extends IDateTimePickerProps<T> {}

/**
 * DateTimeRangePicker Component. Wraps DateTimePicker, which in turn wraps Flatpickr.
 * See IDateTimePickerProps for root property definitions. Can pass Flatpickr
 * properties that are not implemented by this interface via this.flatpickrProps.
 */
@observer
export class DateTimeRangePicker<T> extends React.Component<IDateTimeRangePickerProps<T>> {
	public render() {
		return (
			<DateTimePicker
				/* The two options below are only applied if the humanReadable
				 * property is set to true on Component instantiation. */
				altFormat="h:i K j F, Y"
				dateFormat="Y-m-d H:i"
				/* Set the Flatpickr to allow selection of a datetime range. */
				mode="range"
				{...this.props}
			/>
		);
	}
}
