import React, { useEffect, useState } from "react";
import Navigation from "../Components/Navigation/Navigation";

import { Today } from "@material-ui/icons";
import { useHistory, useLocation } from "react-router";
import Page from "Views/Components/Layout/Page";
import { NewsArticleEntity } from "Models/Entities";
import { store } from "Models/Store";
import { getFetchAllQuery } from "Util/EntityUtils";

export default function News() {
	const history = useHistory();
	const location = useLocation();

	const [state, setstate] = useState<any>([]);

	useEffect(() => {
		store.apolloClient.query({ query: getFetchAllQuery(NewsArticleEntity) }).then((d) => {
			setstate(d.data);
		});
	}, []);

	return (
		<Page title="News">
			<div className="news-page">
				<div className="articles-container">{renderArticles(history, location)}</div>
			</div>
		</Page>
	);
}

function renderArticles(history: any, location: any) {
	// const { open, visible } = getNavigationState(location);

	return articles.map((article: IArticle, index: number) => {
		return <Article key={index} history={history} location={location} article={article} />;
	});
}

export function Article(props: { history: any; location: any; article: IArticle }) {
	const { history, location, article } = props;
	// const { open, visible } = getNavigationState(location);
	return (
		<div
			className="article"
			onClick={() =>
				history.push(article.path, {
					title: article.title,
					content: article.content.toString(),
					articles: articles,
				})
			}>
			<div className="article-image">
				<img src={article.featureImage} />
			</div>
			<div className="article-content">
				<div className="article-information">
					<div className="article-title">{article.title}</div>
					<div className="article-description">{article.description}</div>
				</div>

				<div className="article-date">
					<div className="calendar-icon">
						<Today />
					</div>
					<div className="publish-date">{article.published}</div>
				</div>
			</div>
		</div>
	);
}

// Temporary Filler Objects
export interface IArticle {
	title: string;
	published: string;
	description: string;
	path: string;
	featureImage: string;
	content: string;
}

const articles: IArticle[] = [
	{
		title: "Good Bulls App",
		published: "28th November 2016",
		description:
			"The Good Bulls App makes it easier than ever for dairy farmers to identify bulls that match their breeding priorities.",
		path: convertPath("2016-11-28", "Good Bulls App"),
		featureImage: process.env.PUBLIC_URL + "/news/good-bulls.png",
		content: `The  **Good Bulls App**  makes it easier than ever for dairy farmers to identify bulls that match their breeding priorities.

To download visit [www.adhis.com.au](http://www.adhis.com.au/) and follow the links to the App or Google store.
		
[![goodbulls](http://farmtest.ozlocal.com.au/wp-content/uploads/2016/11/GoodBulls.jpg)](http://www.adhis.com.au/)`,
	},
	{
		title: "Heat Stress in Dairy",
		published: "11th November 2016",
		description:
			"It is that time of the year where the ambient temperature and humidity starts to rise and with that cows start to feel the effects of thermal stress.",
		path: convertPath("2016-11-11", "Heat Stress in Dairy"),
		featureImage:
			"https://www.cowkuhlerz.com/sites/default/files/styles/large/public/2019-08/Heat-Stress%20panting_2.jpg?itok=l80ED054",
		content: `It is that time of the year where the ambient temperature and humidity starts to rise and with that cows start to feel the effects of thermal stress.

Heat stress is undoubtedly one of the major challenges dairy farms face during the summer in Australia. In particular, the climatic conditions along the sub-tropical coastal fringe of New South Wales and Queensland are known to have a lengthy summer season with extended periods of extreme solar radiation and high levels of relative humidity (RH). Five or more months of chronic hot and humid conditions are not uncommon and thus heat stress within these regions is often ceaseless with little relief during the evening, causing accumulative heat load, which dramatically impact on production and profit.

Furthermore, even the more temperate regions of Victoria, South Australia, Southern NSW and Western Australia experience sporadic heat stress events, which may be more detrimental to cows since they have not adapted physiologically to these conditions.

Dairy cows exposed to hot and humid conditions for periods may become heat stressed. Cows react to these stressors by altering behavioural, immunological and physiological functions in order to maintain core body temperature within acceptable ranges. The severity of a thermal impact will depend on an array of factors including; age of cow, lactation status, immunity, nutrition, acclimatisation, genetic and managerial variables. Physiological adaptations to heat stress alter the metabolism and therefore affect how nutrients are utilised by the animal.

As the intensity and duration of the stresses on the cow increases, the ability of the cow to maintain homeostasis is hindered and impaired biological functions result. This is depicted graphically in the figure below (Hahn, 1999). The negative effects of impaired function due to thermal load on the productivity, wellbeing and profitability of the dairy industry are widely known. Other negative effects due to impaired function are depressions in milk fat, protein yields and increases in somatic cell counts and clinical mastitis.

![enter image description here](http://farmtest.ozlocal.com.au/wp-content/uploads/2016/11/HeatStress.jpg)

The heat stress scale (as shown below) can be used to determine the severity of thermal stress. Even at relatively low ambient temperatures of around 25°C with moderate humidity of around 65% can result in a **Thermal Humidity Index (THI)** value of 72, which is described as a mild to moderate heat stress and can have a detrimental impact on the herd. The longer cows are subjected to a specific heat stress level, the higher the losses in productivity and herd health.

![enter image description here](http://farmtest.ozlocal.com.au/wp-content/uploads/2016/11/HSTable.jpg)

###### The table above shows the impact of the various heat stress levels on milk production (Zimbelman and Collier, 2011).

![enter image description here](http://farmtest.ozlocal.com.au/wp-content/uploads/2016/11/HSTable2.jpg)

Strategies to mitigate the negative impact of thermal stress through managerial, genetic and nutritional means can be implemented; however mitigation strategies through genetic selection takes years, so it is easier for farmers to implement managerial and dietary changes to help alleviate the effects of heat stress.

**Managerial strategies:**

It is common knowledge that mechanical means of cooling cows during the summer by providing shade, fans and evaporative cooling by wetting cows or cooling the air by high pressure misting (particularly in more temperate areas) can help to alleviate the effects of heat stress on the herd. However many of these can be costly to the farmer and in particular the increasing cost of electricity makes the running of fans, sprinklers and high pressure misters expensive. Water is also a precious commodity in Australia, therefore limiting its use through the use of sprinklers and high pressure misting for heat abatement. The provision of shade is less expensive and once shade structures have been erected there are no on-going running costs. Providing cows with shade is undoubtedly the most important single managerial decision a farmer can make to help alleviate the stressors of heat. Shade is deemed to be the most effective method of decreasing thermal load since it blocks solar radiation, providing shade to the herd. This should be the first priority.

It is also well known that wetting cows (with adequate air flow) will help alleviate heat stress by reducing body temperature and respiratory rates and therefore has a positive influence on minimising reductions in DMI and milk yield. Evaporative cooling has shown positive improvements in dry matter intakes (DMI) which could account for the milk yields of cooled animals being 42%, 36% and 79% higher on average than those of non-cooled animals during early-, mid- and late-lactation**.** Providing cows with cool, clean water at all times is essential to keep cows hydrated and help to cool cows internally when drinking.

**Nutritional strategies:**

Reduced dry matter intakes is one of the major impacts of thermal stress and contributes to at least 30% of the reductions seen in milk production. Certain nutritional changes may have the potential to exert positive effects during heat stress. Increasing the nutrient density of the diet to counteract lower dry matter intakes as a consequence of heat stress can be done. Successful nutritional strategies will help maintain normal function, which should therefore improve production traits.

A better understanding of animal physiology and the cows’ metabolic processes to cope with heat stress, as well as major advances in animal nutrition have allowed nutritionists to design rations which can help to alleviate the severity of heat stress cows are subjected to during the summer period. Many commonly used feed ingredients, such as electrolytes (sodium bicarbonate and potassium chloride), dietary fats, non-fibre carbohydrates (grains), protein and fibre forages (pasture, silage and hays) can be manipulated to suit the environmental conditions the cow is subjected to. It is commonly known that increasing the nutrient density and feeding quality forages which are low in NDF is an option often employed by nutritionists during the summer months due to the reduced DMI we see at this time of year.

Dietary emphasis should be to increase intake or to alter levels of proteins, amino acids, carbohydrates, fats and other nutrients to improve the conversion of feed units into production units. There is also a wide variety of feed additives available, with many claiming to have positive effects on productivity and feed efficiency when animals are exposed to heat stress, however please take into consideration that there is no single “silver bullet” feed ingredient that will reduce the effects of heat stress. It maybe that the combination of improving diets quality and the use of a few dietary additives provides the best result in reducing the negative effects seen from heat stress. Some feed ingredients that are worth considering are listed below.

**Nutritional feed ingredients to be considered:**

Electrolytes – increase sodium and potassium intake due to increased saliva losses and sweating
Ruminally inert fats – increase the energy density of the diet and generally have 3 x the energy of grains
Betaine – helps reduce cellular osmotic stress, is said to improve gut integrity, improves hydration and acts as a methyl (methionine) donor
Live Yeasts – help improve rumen fermentation, fibre digestion, enhance feed efficiency and help improve rumen pH
Fibrolytic enzymes – increase fibre digestion and enhance feed efficiency
Ruminally inert amino acids – increase essential amino acid density of the diet
Organic selenium – help to improve immune function & acts as an antioxidant. Helps prevent mastitis
Organic zinc – required for keratin formation, helps improve integrity of teat canal and hooves. Helps prevent mastitis
Vitamin E – help to improve immune function & acts as an antioxidant. Helps prevent mastitis.`,
	},
	{
		title: "Lock in Grain Price and Increase Profitability",
		published: "20th October 2016",
		description:
			"Now it might be worth considering locking in concentrate at a good price and increase your farms profitability.",
		path: convertPath("2016-10-20", "Lock in Grain Price and Increase Profitability"),
		featureImage: "https://assets.bwbx.io/images/users/iqjWHBFdfxIU/iuLvZcSC7bhk/v1/1000x-1.jpg",
		content: `Profitability on a dairy farm has many drivers. Some of these are difficult or impossible to control on farm but many are controllable by the farm manager. The first key to understanding profitability is to measure it. This is where programs such as QDAS or similar are excellent tools for understanding what drives profit on your farm.

The 2015 QDAS results showed some interesting key indicators when the top 25% group (sorted by dairy operating profit per cow) were compared to the other 75%. Firstly, what is the “dairy operating profit per cow”? This is a calculation that highlights the amount of profit retained after all expenses (such as, feed, herd costs, dairy electricity, labour, depreciation) are paid. It can be expressed on a per cow basis and it does not include debt servicing and tax payments. It is a measure of how effectively the farm operations generate and retain profits from income.

This year, as we see lower grain prices, there will be a good opportunity to drive dairy operating profit by feeding concentrate. Simple modelling using last year’s QDAS south–east Queensland grazing farms average figures of concentrate fed and using a $120/tonne lower concentrate price of $340/T (compared to $460/T) shows an increase of dairy operating profit of $47,000 (or $215/cow).

This assumes that all other costs have remained the same and the only change has been concentrate price and amount fed. Therefore, pasture utilisation (one of the key profit drivers) stays the same. This is important as we don’t want to compromise pasture utilisation when feeding increased amounts of concentrate.

Table 1 shows how savings can be made to purchased concentrate costs depending on the saving made per tonne and the total amount of concentrate fed. For example, a farmer feeding a total of 200 tonne of concentrate, who saves $120/tonne can potentially make a total saving in purchased concentrate cost of $24,000.

<table width="616">
<tbody>
<tr>
<td rowspan="2" width="94"><strong>Tonnes of Concentrate Fed</strong></td>
<td colspan="7" width="522"><strong>Cost saving per tonne of concentrate</strong></td>
</tr>
<tr>
<td width="68"><strong>40</strong></td>
<td width="76"><strong>60</strong></td>
<td width="76"><strong>80</strong></td>
<td width="76"><strong>100</strong></td>
<td width="76"><strong>120</strong></td>
<td width="76"><strong>140</strong></td>
<td width="76"><strong>160</strong></td>
</tr>
<tr>
<td width="94"><strong>50</strong></td>
<td width="68">2,000</td>
<td width="76">3,000</td>
<td width="76">4,000</td>
<td width="76">5,000</td>
<td width="76">6,000</td>
<td width="76">7,000</td>
<td width="76">8,000</td>
</tr>
<tr>
<td width="94"><strong>100</strong></td>
<td width="68">4,000</td>
<td width="76">6,000</td>
<td width="76">8,000</td>
<td width="76">10,000</td>
<td width="76">12,000</td>
<td width="76">14,000</td>
<td width="76">16,000</td>
</tr>
<tr>
<td width="94"><strong>150</strong></td>
<td width="68">6,000</td>
<td width="76">9,000</td>
<td width="76">12,000</td>
<td width="76">15,000</td>
<td width="76">18,000</td>
<td width="76">21,000</td>
<td width="76">24,000</td>
</tr>
<tr>
<td width="94"><strong>200</strong></td>
<td width="68">8,000</td>
<td width="76">12,000</td>
<td width="76">16,000</td>
<td width="76">20,000</td>
<td width="76">24,000</td>
<td width="76">28,000</td>
<td width="76">32,000</td>
</tr>
<tr>
<td width="94"><strong>250</strong></td>
<td width="68">10,000</td>
<td width="76">15,000</td>
<td width="76">20,000</td>
<td width="76">25,000</td>
<td width="76">30,000</td>
<td width="76">35,000</td>
<td width="76">40,000</td>
</tr>
<tr>
<td width="94"><strong>300</strong></td>
<td width="68">12,000</td>
<td width="76">18,000</td>
<td width="76">24,000</td>
<td width="76">30,000</td>
<td width="76">36,000</td>
<td width="76">42,000</td>
<td width="76">48,000</td>
</tr>
<tr>
<td width="94"><strong>350</strong></td>
<td width="68">14,000</td>
<td width="76">21,000</td>
<td width="76">28,000</td>
<td width="76">35,000</td>
<td width="76">42,000</td>
<td width="76">49,000</td>
<td width="76">54,000</td>
</tr>
<tr>
<td width="94"><strong>400</strong></td>
<td width="68">16,000</td>
<td width="76">24,000</td>
<td width="76">32,000</td>
<td width="76">40,000</td>
<td width="76">48,000</td>
<td width="76">54,000</td>
<td width="76">60,000</td>
</tr>
</tbody>
</table>


Based on this modelling and thinking about the year ahead, now it might be worth considering locking in concentrate at a good price and increase your farms profitability providing pasture utilisation is not compromised. Talk to your grain trader to see what they can do for you. If you need help working out your feed requirements contact your Lactalis Australia Farm Services Officer.`,
	},
];

function convertPath(date: string, title: string) {
	return `/news/${date}/${title.toLowerCase().replace(/\ /gi, "-")}`;
}
