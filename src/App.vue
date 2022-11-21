<template>
	<DillermNavBar :config="config" />
	<div id="container" class="dillerm">
		<div>
		</div>
		<div>
			<dillerm-text
				:class="{ 
					'input-invalid': !input_valid,
					'input-unused': !input_used
				}"
				placeholder="Enter a date here in any format"
				v-model:value="input"
				:debounce-delay="200" />
			<br />
			<div class="combo-table">
				<span>DateTime (Local)</span>
				<div>{{ formatted_date }}</div>
				<span>DateTime (UTC)</span>
				<div>{{ formatted_date_utc }}</div>
				<span>Timestamp</span>
				<div>{{ timestamp }}</div>
				<span>Timestamp (ms)</span>
				<div>{{ timestamp_ms }}</div>
				<span>ISO 8601</span>
				<div>{{ iso_format }}</div>
			</div>
		</div>
		<div>
		</div>
	</div>
</template>

<script>
import DillermSelect from "@dillerm/webutils/src/components/controls/DillermSelect.vue";
import DillermText from "@dillerm/webutils/src/components/controls/DillermText.vue";
import DillermNavBar from "@dillerm/webutils/src/components/DillermNavBar.vue";

// http://www.java2s.com/ref/javascript/javascript-string-format-dict.html
String.prototype.format = function(dict) {
  var result = this;

  if(typeof(dict) === "object") {
	Object.keys(dict).forEach(function(key) {
		result = result.replace("{" + key + "}", dict[key]);
	});//from w ww . j a  va 2 s.  c o  m
	return result;
	}

	var args = [];
	var n = arguments.length;
	var i = 0;

	for(i; i < n; i+=1) {
		args.push(arguments[i]);
	}

	var result = this;

	args.forEach(function(arg) {
		result = result.replace("{}", arg);
	});

	return result;
}


Date.prototype.getTimestamp = function() {
	return parseInt(this.getTime() / 1000);
}

export default {
	components: {
		DillermSelect,
		DillermText,
		DillermNavBar
	},
	data() {
		return {
			config: {
				title: "DateTime Helper",
				github_url: "https://github.com/mdiller/datetime-helper"
			},
			datetime: null,
			input: ""
		}
	},
	computed: {
		formatted_date() {
			return this.formatDate(this.datetime, new Date().getTimezoneOffset());
		},
		formatted_date_utc() {
			return this.formatDate(this.datetime, 0);
		},
		iso_format() {
			return this.datetime.toISOString();
		},
		timestamp() {
			return this.datetime.getTimestamp();
		},
		timestamp_ms() {
			return this.datetime.getTime();
		},
		input_parsed() {
			return this.parseDate(this.input);
		},
		input_valid() {
			return this.input == "" || (this.input_parsed != null);
		},
		input_used() {
			return this.input_valid && this.input != "" && (this.timestamp == this.input_parsed.getTimestamp())
		}
	},
	watch: {
		input_parsed() {
			if (this.input_parsed != null) {
				this.datetime = new Date(this.input_parsed.getTime());
			}
		}
	},
	methods: {
		pasteHandler(event) {
			var text = (event.clipboardData || window.clipboardData).getData('text');
			if (text && event.target.tagName != "INPUT") {
				this.input = text;
			}
		},
		parseDate(text) {
			text = text.trim();
			text = text.replace(/(?<=[^\s])-(?=[^\s])/g, " "); // replace - inbetween 2 non-spaces with " ".
			if (/^[0-9\.]+$/.test(text)) {
				var value = parseFloat(text);
				if (value < 100000000000) {
					// this is probably in seconds, not ms.
					value *= 1000;
				}
				text = parseInt(value);
			}
			var date = new Date(text);
			return (Object.prototype.toString.call(date) === "[object Date]" && !isNaN(date)) ? date : null;
		},
		formatDate(date, tz_offset = 0) {
			// https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Date
			var current_tz_offset = date.getTimezoneOffset();
			if (tz_offset != current_tz_offset) {
				var offset_diff = current_tz_offset - tz_offset;
				date = new Date(date.getTime() + (offset_diff * 60000));
			}
			var date_parts = {
				day: date.getDate(),
				month: date.getMonth(),
				year: date.getFullYear(),
				hour: date.getHours(),
				minute: date.getMinutes().toString().padStart(2, "0"),
				second: date.getSeconds(),
			}
			if (date_parts.hour < 12) {
				date_parts.am_pm = "AM";
				if (date_parts.hour == 0) {
					date_parts.hour = 12;
				}
			}
			else {
				date_parts.am_pm = "PM";
				if (date_parts.hour != 12) {
					date_parts.hour -= 12;
				}
			}
			var shortMonth = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul","Aug", "Sep", "Oct", "Nov", "Dec"];
			date_parts.month_short = shortMonth[date_parts.month];
			var format_string = `{day}-{month_short}-{year} {hour}:{minute} {am_pm}`;
			return format_string.format(date_parts);
		}
	},
	created() {
		this.datetime = new Date();

		document.removeEventListener('paste', this.pasteHandler);
		document.addEventListener('paste', this.pasteHandler);
	}
};


</script>

<style>

:root {
	--slider-opacity: 75%;
	--animation-time: 0.15s;
}

#container {
	width: 100%;
	display: flex;
}

#container > div {
	flex: 1;
	padding: 10px 50px;
}

.combo-table { 
	display: grid;
    grid-template-columns: min-content auto; 
	background: var(--background-color3);
	border: 2px solid var(--background-color3);
	border-radius: 8px;
	gap: 1px
}

.combo-table > * {
	padding: 12px;
}

.combo-table > span {
	background: var(--background-color1);
	text-align: left;
	white-space: nowrap;
}

.combo-table > div {
	background: var(--background-color2);
	text-align: right;
}

.combo-table > :nth-child(1) { border-radius: 8px 0px 0px 0px; }
.combo-table > :nth-child(2) { border-radius: 0px 8px 0px 0px; }
.combo-table > :nth-last-child(1) { border-radius: 0px 0px 8px 0px; }
.combo-table > :nth-last-child(2) { border-radius: 0px 0px 0px 8px; }

@media screen and (max-width: 700px) {
	#container {
		flex-direction: column;
	}
	#container > div {
		padding: 10px;
	}
	#container > div:nth-child(2) {
		order: -1;
	}
}

input[type="datetime-local"] {
	cursor: pointer;
	width: 100%;
	background-color: var(--input-background);
	border: var(--input-border);
	border-radius: var(--input-border-radius);
	line-height: calc(var(--input-height) - 2 * var(--input-border-size));
	padding: 0px 8px;
	color: var(--input-text-color);
}

.input-invalid > input{
	box-shadow: 0px 0px 10px red;
}

.input-unused > input{
	color: grey;
}

</style>