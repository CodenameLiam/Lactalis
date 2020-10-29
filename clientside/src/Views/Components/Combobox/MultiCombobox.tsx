import * as React from "react";
import { observer } from "mobx-react";
import { action } from "mobx";
import { AsyncComboboxProps, Combobox, SyncComboboxProps } from "./Combobox";
import { DropdownProps } from "semantic-ui-react";

export interface SyncMultiComboboxProps<T, I> extends SyncComboboxProps<T, I> {}

export interface AsyncMultiComboboxProps<T, I> extends AsyncComboboxProps<T, I> {}

export type IMultiComboboxProps<T, I> = SyncMultiComboboxProps<T, I> | AsyncComboboxProps<T, I>;

/**
 * A MultiCombobox is a view that allows allows selection of many elements from a dropdown menu
 */
@observer
export class MultiCombobox<T, I> extends React.Component<IMultiComboboxProps<T, I>> {
	static defaultProps = {
		styles: {},
	};

	public render() {
		// return "Not yet done";
		return (
			<Combobox
				{...this.props}
				inputProps={{
					multiple: true,
					...this.props.inputProps,
				}}
			/>
		);
	}
}
