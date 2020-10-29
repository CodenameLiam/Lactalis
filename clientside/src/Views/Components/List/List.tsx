import * as React from "react";
import { observer } from "mobx-react";

export interface IItemProps<T> extends React.HTMLAttributes<Element> {
	item: T;
}

export interface IListProps<Item> extends React.HTMLAttributes<Element> {
	collection: Item[];
	emptyView: typeof React.Component;
	itemView: typeof React.Component;
}

@observer
export default class List<Item> extends React.Component<IListProps<Item>, any> {
	public render() {
		if (this.props.collection.length) {
			return this.props.collection.map((r, idx) => <this.props.itemView key={idx} item={r} />);
		} else {
			return <this.props.emptyView />;
		}
	}
}
