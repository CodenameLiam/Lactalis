import * as React from "react";
import classNames from "classnames";
import { IFilter } from "../CollectionFilterPanel";
import { observer } from "mobx-react";
import { computed } from "mobx";
import { SyncComboboxProps } from "Views/Components/Combobox/Combobox";
import { MultiCombobox } from "Views/Components/Combobox/MultiCombobox";

interface IFilterEnumComboBoxProps<T, I> extends Partial<SyncComboboxProps<T, I>> {
	filter: IFilter<T>;
	className?: string;
}

@observer
class FilterEnumComboBox<T, I> extends React.Component<IFilterEnumComboBoxProps<T, I>> {
	@computed
	private get options() {
		return this.props.filter.enumResolveFunction || [];
	}

	public render() {
		const { filter, className } = this.props;

		return (
			<MultiCombobox
				model={filter}
				modelProperty="value1"
				label={filter.displayName}
				className={classNames("collection-filter-enum-combobox", className)}
				options={this.options}
				isClearable={true}
				onAfterChange={(event, data) => {
					filter.active = !!filter.value1 && (filter.value1 as string[]).length > 0;
					if (this.props.onAfterChange) {
						this.props.onAfterChange(event, data);
					}
				}}
			/>
		);
	}
}

export default FilterEnumComboBox;
