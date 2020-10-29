import { IconButton } from "@material-ui/core";
import { List } from "@material-ui/icons";
import { gql } from "apollo-boost";
import { NewsArticleEntity } from "Models/Entities";
import { store } from "Models/Store";
import moment from "moment";
import { Moment } from "moment";
import React, { useEffect, useState } from "react";
import ComponentSpinner from "../Spinner/ComponentSpinner";
import PageSpinner from "../Spinner/PageSpinner";
import MilkGraph from "./MilkGraph";
import StateAverageGraph from "./StateAverageGraph";

export interface IFarm {
	id: string;
	name: string;
	state: string;
}

interface ICharts {
	farm: IFarm;
}

export default function Charts(props: ICharts) {
	const [pickups, setPickups] = useState<any>(undefined);

	useEffect(() => {
		if (props.farm) {
			store.apolloClient.query({ query: queryPickups(props.farm.id) }).then((d) => {
				setPickups(d.data.farmEntity.pickupss);
			});
		}
	}, [props.farm]);

	if (!props.farm || !pickups) {
		return <ComponentSpinner />;
	}

	return (
		<div className="charts">
			<MilkGraph
				records={pickups}
				type="volume"
				title="Volume (L)"
				borderColor="rgba(113, 210, 245, 1)"
				backgroundColor="rgba(113, 210, 245, 0.7)"
			/>
			<MilkGraph
				records={pickups}
				type="protein"
				title="Protein (%)"
				borderColor="rgba(146, 243, 230, 1)"
				backgroundColor="rgba(146, 243, 230, 0.7)"
			/>
			<MilkGraph
				records={pickups}
				type="milkFat"
				title="Fat (%)"
				borderColor="rgba(255,149,174,1)"
				backgroundColor="rgba(255,149,174,0.7)"
			/>
			<MilkGraph
				records={pickups}
				type="temperature"
				title="Temperature (C)"
				borderColor="rgba(67,136,244,1)"
				backgroundColor="rgba(67,136,244,0.7)"
			/>
			<StateAverageGraph state={props.farm.state} records={pickups} />
			<FeaturedStory />
			<RecentStories />
		</div>
	);
}

function RecentStories() {
	const [articles, setArticles] = useState<NewsArticleEntity[] | undefined>(undefined);

	useEffect(() => {
		store.apolloClient.query({ query: queryRecentArticles() }).then((d) => {
			setArticles(d.data.newsArticleEntitys);
		});
	}, []);

	return (
		<div className="info-card-wide info-card-news">
			<div className="info-head info-head-no-margin">
				<div className="info-title">Recent Stories</div>
				<div className="info-setting-formatter" />
			</div>
			<div className="recent-articles">{renderArticles()}</div>
		</div>
	);

	function renderArticles() {
		if (articles) {
			return articles.map((article) => (
				<div
					className="featured-story"
					onClick={() => store.routerHistory.push(`/news/${article.id}`)}>
					<img className="article-image" src={`/api/files/${article.featureImageId}`} />
					<div className="article-text">
						<div className="article-title">{article.headline}</div>
						<div className="article-description">{article.description}</div>
					</div>
				</div>
			));
		}
		return <ComponentSpinner />;
	}
}

function queryRecentArticles() {
	return gql`
		query RecentNews {
			newsArticleEntitys(orderBy: { path: "created", descending: true }, take: 2) {
				id
				featureImageId
				headline
				description
				created
			}
		}
	`;
}

function FeaturedStory() {
	const [article, setArticle] = useState<NewsArticleEntity | undefined>(undefined);

	useEffect(() => {
		store.apolloClient.query({ query: queryFeaturedArticle() }).then((d) => {
			setArticle(d.data.promotedArticlesEntity.newsArticless[0]);
		});
	}, []);

	return (
		<div className="info-card info-card-news">
			<div className="info-head info-head-no-margin">
				<div className="info-title">Featured Story</div>
				<div className="info-setting-formatter" />
			</div>
			{renderArticle()}
		</div>
	);

	function renderArticle() {
		if (article) {
			return (
				<div
					className="featured-story"
					onClick={() => store.routerHistory.push(`/news/${article.id}`)}>
					<img className="article-image" src={`/api/files/${article.featureImageId}`} />
					<div className="article-text">
						<div className="article-title">{article.headline}</div>
						<div className="article-description">{article.description}</div>
					</div>
				</div>
			);
		}
		return <ComponentSpinner />;
	}
}

function queryFeaturedArticle() {
	return gql`
		query FeaturedNews {
			promotedArticlesEntity(take: 1) {
				newsArticless(take: 1) {
					id
					description
					headline
					featureImageId
					created
				}
			}
		}
	`;
}

function queryPickups(farmId: string) {
	return gql`
		query Pickups {
			farmEntity(id: "${farmId}") {
				id
				pickupss (orderBy: {path: "time", descending: true }) {
					time
					milkFat
					temperature
					protein
					volume
				}
			}
		}
	`;
}
