/**
 * Runs a map function over an iterator, returning an iterator of the mapped values
 * @param iterator The iterable to map over
 * @param callback The mapping function
 * @returns An iterator of the mapped values
 */
export function* LazyMap<T, U>(iterator: Iterable<T>, callback: (item: T) => U): Iterable<U> {
	for (const item of iterator) {
		yield callback(item);
	}
}

/**
 * Runs a map function over an iterator, returning an array of the mapped values
 * @param iterator The iterable to map over
 * @param callback The mapping function
 * @returns An array of the mapped values
 */
export function IterableMap<T, U>(iterator: Iterable<T>, callback: (item: T) => U): U[] {
	return Array.from(LazyMap(iterator, callback));
}

/**
 * Runs a filter function over an iterator, returning an iterator of the filtered values
 * @param iterator The iterable to filter over
 * @param predicate The filter predicate
 * @returns An iterator of the filtered values
 */
export function* LazyFilter<T>(
	iterator: Iterable<T>,
	predicate: (item: T) => boolean
): Iterable<T> {
	for (const item of iterator) {
		if (predicate(item)) {
			yield item;
		}
	}
}

/**
 * Runs a filter function over an iterator, returning an array of the filtered values
 * @param iterator The iterable to filter over
 * @param predicate The filter predicate
 * @returns An array of the filtered values
 */
export function IterableFilter<T>(iterator: Iterable<T>, predicate: (item: T) => boolean): T[] {
	return Array.from(LazyFilter(iterator, predicate));
}

/**
 * Finds the first itemn in an iterable that matches a predicate
 * @param iterator The iterable to find the value in
 * @param predicate The predicate to find by
 * @returns The first item to match the predicate or undefined if there is no match
 */
export function IterableFind<T>(
	iterator: Iterable<T>,
	predicate: (item: T) => boolean
): T | undefined {
	for (const item of iterator) {
		if (predicate(item)) {
			return item;
		}
	}
	return undefined;
}

export function IterableValuesArray<T>(iterator: Iterable<T>): T[] {
	const results = [];
	for (const item of iterator) {
		results.push(item);
	}
	return results;
}

function isObject(obj: any) {
	const type = typeof obj;
	return type === "function" || (type === "object" && !!obj);
}

function isArray(arr: any) {
	const arrayFunc =
		Array.isArray ||
		((obj: any) => {
			return toString.call(obj) === "[object Array]";
		});
	return arrayFunc(arr);
}

export function safeLookup(obj: any, ...lookup: any[]) {
	let result = obj;
	for (const item of lookup) {
		if (result === null || result === undefined) {
			return result;
		}
		if (isObject(item)) {
			const keys = Object.keys(item);
			if (keys.length > 0) {
				if (typeof result[keys[0]] !== "function") {
					return undefined;
				}

				let v;
				if (isArray(item[keys[0]])) {
					v = item[keys[0]];
				} else {
					v = [item[keys[0]]];
				}
				result = result[keys[0]].apply(result, v);
			}
		} else {
			result = result[item];
		}
	}
	return result;
}
