import * as React from "react";
import { IFilter } from "../CollectionFilterPanel";
import { observer } from "mobx-react";
import { observable, action } from "mobx";
import { DateRangePicker, IDateRangePickerProps } from "../../DateRangePicker/DateRangePicker";
import classnames from "classnames";
import moment from "moment";
import { Instance } from "flatpickr/dist/types/instance";

interface IFilterDateRangeProps<T> extends Partial<IDateRangePickerProps<T>> {
	filter: IFilter<T>;
	className?: string;
}

@observer
class FilterDateRange<T> extends React.Component<IFilterDateRangeProps<T>> {
	@observable
	private model: { dateRange: [Date | undefined, Date | undefined] } = {
		dateRange: [undefined, undefined],
	};

	constructor(props: IFilterDateRangeProps<T>, context: any) {
		super(props, context);
		this.setDefaultValue();
	}

	@action
	componentDidUpdate() {
		this.setDefaultValue();
	}

	private setDefaultValue() {
		this.model.dateRange = [
			!!this.props.filter.value1 ? moment(this.props.filter.value1).toDate() : undefined,
			!!this.props.filter.value2 ? moment(this.props.filter.value2).toDate() : undefined,
		];
	}

	public render() {
		const { filter, className } = this.props;
		const classes = classnames("collection-filter-date-range", className);
		return (
			<DateRangePicker
				model={this.model}
				modelProperty="dateRange"
				label={filter.displayName}
				className={classes}
				onAfterChange={(dates: Date[], currentDateString: string, self: Instance, data?: any) => {
					filter.value1 = !!this.model.dateRange[0]
						? moment(this.model.dateRange[0]).format("YYYY-MM-DD")
						: undefined;
					filter.value2 = !!this.model.dateRange[1]
						? moment(this.model.dateRange[1]).format("YYYY-MM-DD")
						: undefined;
					filter.active = !!filter.value1 && !!filter.value2;
					if (this.props.onAfterChange) {
						this.props.onAfterChange(dates, currentDateString, self, data);
					}
				}}
			/>
		);
	}
}

export default FilterDateRange;
