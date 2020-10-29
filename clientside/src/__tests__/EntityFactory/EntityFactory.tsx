import { IModelType, Model } from "Models/Model";

export class EntityFactory<T extends Model> {
	private _useAttributes: boolean = true;
	private _userReferences: boolean = true;
	private _totalEntities: number = 1;

	constructor(private model: IModelType<T>) {}

	public SetAmount = (totalEntities: number) => {
		this._totalEntities = totalEntities;
	};

	public UseAttributes = (enabled?: boolean): EntityFactory<T> => {
		this._useAttributes = !(enabled === false);
		return this;
	};

	public UseReferences = (enabled?: boolean): EntityFactory<T> => {
		this._userReferences = !(enabled === false);
		return this;
	};

	public Generate = (): Array<T> => {
		const something = new this.model();
		let dlkfjsdl = something.attributeTypes;
		return [something];
	};
}
