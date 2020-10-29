import { default as ApolloClient, NetworkStatus } from "apollo-boost";
import { QueryOptions } from "apollo-client/core/watchQueryOptions";
import { ApolloQueryResult } from "apollo-client/core/types";
import { store } from "Models/Store";
import { SERVER_URL } from "Constants";

export const pollingInterval = 100;
export const pollingTimeout = 1000;

let storedGraphqlResponses: { [key: string]: {} } = {};

export function setupGraphQlMocking(graphqlResponses: { [key: string]: {} }) {
	storedGraphqlResponses = { ...storedGraphqlResponses, ...graphqlResponses };

	store.apolloClient = new ApolloClient({
		uri: `${SERVER_URL}/api/graphql`,
	});

	store.apolloClient.query = (options: QueryOptions): Promise<ApolloQueryResult<any>> => {
		return new Promise<ApolloQueryResult<any>>((resolve, reject) => {
			const queryName = options.query.definitions[0]?.["name"]?.["value"];
			if (!queryName) {
				return reject("Query name undefined");
			}
			const graphqlResponse = storedGraphqlResponses[queryName];
			if (!graphqlResponse) {
				return reject("Mocked response is undefined");
			}
			return resolve({
				data: graphqlResponse,
				loading: false,
				stale: false,
				networkStatus: NetworkStatus.ready,
			});
		});
	};
}
