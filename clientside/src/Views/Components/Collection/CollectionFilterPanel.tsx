import { observer } from "mobx-react";
import * as React from "react";
import { Comparators } from "../ModelCollection/ModelQuery";
import { displayType } from "Models/CRUDOptions";
import { Button, Display } from "../Button/Button";
import FilterDateRange from "./CollectionFilterAttributes/FilterDateRangePicker";
import FilterEnumComboBox from "./CollectionFilterAttributes/FilterEnumComboBox";
import _ from "lodash";
import { ButtonGroup, Alignment } from "../Button/ButtonGroup";

export interface IFilter<T> {
	/** The column name to filter on */
	path: string;
	/** The comparison operator */
	comparison: Comparators | "range";
	/** The value to filter on */
	value1: string | string[] | Date | number | undefined;
	/**
	 * The second value to filter
	 * Only valid for 'range' type comparison for now where this represents the end of the range
	 */
	value2: string | Date | number | undefined;
	/** this is specifically for the model of date range */
	active: boolean;
	/** The display type of the filter */
	displayType: displayType;
	/** The display name of the filter */
	displayName: string;
	/** The function to resolve and return the options of the enum-combobox (for now only enum-combobox) */
	enumResolveFunction?: Array<{ display: string; value: string }>;
}

export interface ICollectionFilterPanelProps<T> {
	filters: IFilter<T>[];
	onClearFilter: () => void;
	onApplyFilter: () => void;
	onFilterChanged?: () => void;
}

@observer
class CollectionFilterPanel<T> extends React.Component<ICollectionFilterPanelProps<T>> {
	public render() {
		const { filters, onFilterChanged, onApplyFilter, onClearFilter } = this.props;

		if (filters === undefined || !filters.length) {
			return null;
		}

		return (
			<>
				<div className="collection-filter-form__container">
					{filters.map((filter) => {
						switch (filter.displayType) {
							case "datepicker":
								if (filter.comparison === "range") {
									return (
										<FilterDateRange
											filter={filter}
											className={"filter-" + filter.path}
											key={"filter-" + filter.path}
											onAfterChange={() => {
												if (onFilterChanged) {
													onFilterChanged();
												}
											}}
										/>
									);
								}
								return "";
							case "enum-combobox":
								return (
									<FilterEnumComboBox
										filter={filter}
										className={"filter-" + filter.path}
										key={"filter-" + filter.path}
										onAfterChange={() => {
											if (onFilterChanged) {
												onFilterChanged();
											}
										}}
									/>
								);

							default:
								console.error(`The filter display type ${filter.displayType} is not supported.`);
								return "";
						}
					})}
				</div>
				<div className="collection-filter-form__actions">
					<ButtonGroup alignment={Alignment.HORIZONTAL}>
						<Button className="clear-filters" display={Display.Outline} onClick={onClearFilter}>
							Clear Filters
						</Button>
						<Button className="apply-filters" display={Display.Solid} onClick={onApplyFilter}>
							Apply Filters
						</Button>
					</ButtonGroup>
				</div>
			</>
		);
	}
}

export default CollectionFilterPanel;
