<template>
	<div class="clock">
		<span
			class="clock-hand"
			:style="{ '--rotation': minute_degrees }">
		</span>
		<span	
			class="clock-hand small"
			:style="{ '--rotation': hour_degrees }">>
		</span>
		<div>
			<div>
				<div>
				</div>
			</div>
		</div>
	</div>
</template>

<script>

export default {
	name: 'clock',
	props: {
		value: { // datetime
			type: Object,
			required: true
		}
	},
	data() {
		return {
			use_local: true
		};
	},
	computed: {
		hour_degrees() {
			var hour = this.use_local ? this.value.getHours() : this.value.getUTCHours();
			hour = hour % 12;
			var degrees = ((hour / 12) * 360).toFixed();
			return `${degrees}deg`;
		},
		minute_degrees() {
			var minute = this.use_local ? this.value.getMinutes() : this.value.getUTCMinutes();
			var degrees = ((minute / 60) * 360).toFixed();
			return `${degrees}deg`;
		}
	}
};


</script>

<style lang="scss">

.clock {
	--clock-size: 300px;
	position: relative;
	width: var(--clock-size);
	height: var(--clock-size);
}

.clock > div,
.clock > div > div,
.clock > div > div > div {
	width: 100%;
	height: 100%;
	user-select: none;
}

.clock > div {
	background: var(--background-color3);
	border-radius: var(--clock-size);
	padding: calc(var(--clock-size) / 20);
}

.clock > div > div {
	background: var(--background-color2);
	border-radius: var(--clock-size);
	display: flex;
	justify-content: center;
	align-items: center;
}

.clock > div > div > div {
	background: var(--background-color3);
	--size: calc(var(--clock-size) / 10);
	width: var(--size);
	height: var(--size);
	border-radius: var(--size);
	z-index: 10;
}

.clock-hand {
	user-select: none;
	background: var(--background-color4);
	height: calc(var(--clock-size) / 20);
	width: calc(var(--clock-size) * 0.4);
	border-radius: calc(var(--clock-size) / 75);
	position: absolute;
	left: 50%;
	top: 50%;
	transition: transform var(--animation-time);
	transform-origin: 0 0;
	transform: rotate(calc(var(--rotation) - 90deg)) translate(0%, -50%);
	cursor: grab;
	// box-shadow: 0 0 2px black;

	&.small {
		width: calc(var(--clock-size) * 0.25);
	}
}

</style>