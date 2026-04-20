<script setup lang="ts">
import WeatherDashboard from "@/components/WeatherDashboard.vue";
import { useWeatherApi } from "@/composables/useWeatherApi";

const { data, loading, error, fetchWeatherApiData} = useWeatherApi()
</script>

<template>
  <div class="app-root">
    <Transition name="fade" mode="out-in">

      <div v-if="loading" key="loading" class="screen-center">
        <div class="spinner" />
        <p class="hint">Загрузка погоды…</p>
      </div>

      <div v-else-if="error" key="error" class="screen-center">
        <span class="err-icon">⛈️</span>
        <p class="err-text">{{ error }}</p>
        <button class="retry-btn" @click="fetchWeatherApiData">↺ Повторить</button>
      </div>

      <WeatherDashboard v-else-if="data" key="data" :data="data!" />

    </Transition>
  </div>
</template>

<style scoped>
header {
  line-height: 1.5;
  max-height: 100vh;
}

.logo {
  display: block;
  margin: 0 auto 2rem;
}

nav {
  width: 100%;
  font-size: 12px;
  text-align: center;
  margin-top: 2rem;
}

nav a.router-link-exact-active {
  color: var(--color-text);
}

nav a.router-link-exact-active:hover {
  background-color: transparent;
}

nav a {
  display: inline-block;
  padding: 0 1rem;
  border-left: 1px solid var(--color-border);
}

nav a:first-of-type {
  border: 0;
}

@media (min-width: 1024px) {
  header {
    display: flex;
    place-items: center;
    padding-right: calc(var(--section-gap) / 2);
  }

  .logo {
    margin: 0 2rem 0 0;
  }

  header .wrapper {
    display: flex;
    place-items: flex-start;
    flex-wrap: wrap;
  }

  nav {
    text-align: left;
    margin-left: -1rem;
    font-size: 1rem;

    padding: 1rem 0;
    margin-top: 1rem;
  }
}
</style>
