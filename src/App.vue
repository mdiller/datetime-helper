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
			<div class="combo-table">
				<span>DateTime (Local)</span>
				<div>{{ formatted_date }}</div>
				<span>DateTime (UTC)</span>
				<div>{{ formatted_date }}</div>
				<span>Timestamp</span>
				<div>{{ timestamp }}</div>
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
			return this.datetime.toLocaleDateString('en-us', { weekday:"long", year:"numeric", month:"short", day:"numeric"});
		},
		formatted_date_utc() {
			return this.datetime.toLocaleDateString('en-us', { weekday:"long", year:"numeric", month:"short", day:"numeric"});
		},
		iso_format() {
			return this.datetime.toISOString();
		},
		timestamp() {
			return this.datetime.getTime();
		},
		input_parsed() {
			var text = this.input.trim();
			var date = new Date(text);
			return (Object.prototype.toString.call(date) === "[object Date]" && !isNaN(date)) ? date : null;
		},
		input_valid() {
			return this.input == "" || (this.input_parsed != null);
		},
		input_used() {
			return this.input_valid && this.input != "" && (this.timestamp == this.input_parsed.getTime())
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
				console.log("pasted");
				this.input = text;
			}
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