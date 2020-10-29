export default class InputsHelper {
	public static getAriaDescribedBy(id: string, tooltip?: string, subDescription?: string) {
		const tooltipId = `${id}-tooltip`;
		const subDescriptionId = `${id}-sub-description`;
		const ariaDescribedBy = tooltip ? tooltipId : subDescription ? subDescriptionId : undefined;
		return ariaDescribedBy;
	}
}
