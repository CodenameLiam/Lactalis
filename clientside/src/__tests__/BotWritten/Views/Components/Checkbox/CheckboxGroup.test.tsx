import { mount } from "enzyme";
import React from "react";
import { Checkbox } from "Views/Components/Checkbox/Checkbox";
import { CheckboxGroup } from "Views/Components/Checkbox/CheckboxGroup";

describe("Checkbox Group Component", () => {
	it("a change event on each checkbox, only affects one of the checkboxes", () => {
		let model = {
			item1: false,
			item2: false,
			item3: false,
		};

		let group = (
			<CheckboxGroup label="Header for this checkbox group">
				<Checkbox
					model={model}
					id="checkbox-1"
					modelProperty={"item1"}
					label={"General Checkbox"}></Checkbox>
				<Checkbox
					model={model}
					id="checkbox-2"
					modelProperty={"item2"}
					label={"General Checkbox"}></Checkbox>
				<Checkbox
					model={model}
					id="checkbox-3"
					modelProperty={"item3"}
					label={"General Checkbox"}></Checkbox>
			</CheckboxGroup>
		);

		const component = mount(group);

		component.find("#checkbox-1-field").simulate("change", { target: { checked: true } });
		expect(model.item1).toEqual(true);
		expect(model.item2).toEqual(false);
		expect(model.item3).toEqual(false);
		component.find("#checkbox-2-field").simulate("change", { target: { checked: true } });
		expect(model.item1).toEqual(true);
		expect(model.item2).toEqual(true);
		expect(model.item3).toEqual(false);
		component.find("#checkbox-3-field").simulate("change", { target: { checked: true } });
		expect(model.item1).toEqual(true);
		expect(model.item2).toEqual(true);
		expect(model.item3).toEqual(true);
	});
});
