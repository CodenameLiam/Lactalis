import { pollingInterval, pollingTimeout } from "__tests__/TestConfig/TestConfig";
import { ReactWrapper } from "enzyme";

export const sleep = (milliseconds: number) => {
	return new Promise((resolve) => setTimeout(resolve, milliseconds));
};

export const pollComponent = async (
	component: ReactWrapper<any, any>,
	assertion: () => boolean
) => {
	let pollCount = 0;
	while (pollCount < pollingTimeout / pollingInterval) {
		component.update();
		if (assertion()) {
			return;
		}
		await sleep(pollingInterval);
		pollCount++;
	}
	throw "Assertion Failed";
};
