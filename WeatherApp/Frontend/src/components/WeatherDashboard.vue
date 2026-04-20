<script setup lang="ts">
import { computed } from 'vue'
import type {WeatherApiData, Hour } from '@/types/weatherapidata'

const props = defineProps<{data: WeatherApiData}>()
const current = computed(() => props.data.current)

const round    = (n: number): number  => Math.round(n)
const cap      = (s: string): string  => s.charAt(0).toUpperCase() + s.slice(1)
const iconUrl  = (icon: string): string => `https:${icon}` // WeatherAPI уже даёт путь к иконке

const fmtTime  = (time: string): string => time.slice(-5) // "2024-04-16 14:00" → "14:00"
const fmtDay   = (date: string): string =>
  new Intl.DateTimeFormat('ru-RU', { weekday: 'long' }).format(new Date(date))

const currentDate = computed(() =>
  new Intl.DateTimeFormat('ru-RU', {
    weekday: 'long', day: 'numeric', month: 'long'
  }).format(new Date(current.value.last_updated))
)

const details = computed(() => {
  const c = current.value
  return [
    { icon: '💧', value: `${c.humidity}%`,            label: 'Влажность' },
    { icon: '💨', value: `${round(c.wind_kph)} км/ч`, label: 'Ветер'     },
    { icon: '☁️', value: `${c.cloud}%`,                label: 'Облачность'},
    { icon: '🌡', value: `${round(c.feelslike_c)}°`,  label: 'Ощущается' },
  ]
})

const filteredHourly = computed(() => {
  const now = new Date()
  const todayHours = props.data.forecast.forecastday[0]?.hour ?? []
  const tomorrowHours = props.data.forecast.forecastday[1]?.hour ?? []

  const remaining = todayHours.filter((h: Hour) => {
    const hTime = new Date(h.time)
    return hTime >= now
  })

  return [...remaining, ...tomorrowHours]
})


const threeDays = computed(() => props.data.forecast.forecastday.slice(0, 3))
</script>

<template>

  <div class="dashboard">

    <header class="city-header">
      <h1 class="city-name">Москва</h1>
      <p class="city-date">{{currentDate}}</p>
    </header>

    <section class="card">
      <div class="cur-main">
        <img :src="iconUrl(current.condition.icon)" class="cur-icon" :alt="current.condition.text" />
        <div>
          <div class="cur-temp">{{round(current.temp_c)}}°</div>
          <div class="cur-desc">{{current.condition.text}}</div>
          <div class="cur-feels">Ощущается как {{round(current.feelslike_c)}}°</div>
        </div>
      </div>

      <div class="cur-details">
        <div class="det" v-for="item in details" :key="item.label">
          <span class="det-ico">{{ item.icon }}</span>
          <span class="det-val">{{ item.value }}</span>
          <span class="det-lbl">{{ item.label }}</span>
        </div>
      </div>
    </section>

    <!-- По часам -->
    <section class="card">
      <p class="sec-title">По часам</p>
      <div class="hourly-scroll">
        <div v-for="h in filteredHourly" :key="h.time" class="hcard">
          <span class="hcard-time">{{ fmtTime(h.time) }}</span>
          <img :src="iconUrl(h.condition.icon)" class="hcard-ico" />
          <span class="hcard-temp">{{ round(h.temp_c) }}°</span>
        </div>
      </div>
    </section>

    <!-- Прогноз на 3 дня -->
    <section class="card">
      <p class="sec-title">Прогноз на 3 дня</p>
      <div class="daily-list">
        <div v-for="d in threeDays" :key="d.date" class="drow">
          <span class="drow-name">{{ fmtDay(d.date) }}</span>
          <img :src="iconUrl(d.day.condition.icon)" class="drow-ico" />
          <span class="drow-desc">{{ cap(d.day.condition.text) }}</span>
          <div class="drow-temps">
            <span class="dmax">{{ round(d.day.maxtemp_c) }}°</span>
            <span class="dmin">{{ round(d.day.mintemp_c) }}°</span>
          </div>
        </div>
      </div>
    </section>

  </div>

</template>

<style scoped>
.dashboard {
  max-width: 480px;
  margin: 0 auto;
  padding: 28px 16px 48px;
  display: flex; flex-direction: column; gap: 16px;
}

/* Шапка */
.city-header { text-align: center; padding: 4px 0 8px; }
.city-name   { font-size: 2rem; font-weight: 600; letter-spacing: .5px; }
.city-date   { color: rgba(255,255,255,.5); font-size: .9rem; margin-top: 4px; text-transform: capitalize; }

/* Карточки */
.card {
  background: rgba(255,255,255,.06);
  backdrop-filter: blur(16px);
  border: 1px solid rgba(255,255,255,.1);
  border-radius: 22px;
  padding: 20px;
}

.sec-title {
  font-size: .72rem; font-weight: 500;
  text-transform: uppercase; letter-spacing: 1.6px;
  color: rgba(255,255,255,.4); margin-bottom: 14px;
}

/* Текущая */
.cur-main  { display: flex; align-items: center; gap: 4px; margin-bottom: 18px; }
.cur-icon  { width: 96px; height: 96px; filter: drop-shadow(0 0 14px rgba(96,165,250,.4)); }
.cur-temp  { font-size: 4.5rem; font-weight: 200; line-height: 1; }
.cur-desc  { font-size: 1.05rem; color: rgba(255,255,255,.8); margin-top: 4px; }
.cur-feels { font-size: .82rem; color: rgba(255,255,255,.45); margin-top: 2px; }

.cur-details {
  display: grid; grid-template-columns: repeat(4,1fr); gap: 8px;
  padding-top: 16px; border-top: 1px solid rgba(255,255,255,.07);
}
.det      { display: flex; flex-direction: column; align-items: center; gap: 3px; }
.det-ico  { font-size: 1.1rem; }
.det-val  { font-size: .85rem; font-weight: 500; }
.det-lbl  { font-size: .68rem; color: rgba(255,255,255,.38); }

/* Почасовой */
.hourly-scroll {
  display: flex; gap: 8px;
  overflow-x: auto; padding-bottom: 4px;
  scrollbar-width: thin; scrollbar-color: rgba(255,255,255,.12) transparent;
}

.hcard {
  flex: 0 0 auto; min-width: 62px;
  display: flex; flex-direction: column; align-items: center; gap: 4px;
  padding: 10px 10px; background: rgba(255,255,255,.05); border-radius: 16px;
}
.hcard-time { font-size: .75rem; color: rgba(255,255,255,.5); }
.hcard-ico  { width: 36px; height: 36px; }
.hcard-temp { font-size: .9rem; font-weight: 500; }
.hcard-pop  { font-size: .72rem; color: #93c5fd; }

/* Трёхдневный */
.daily-list { display: flex; flex-direction: column; gap: 14px; }

.drow {
  display: flex; align-items: center; gap: 10px;
}
.drow-name { flex: 0 0 80px; font-size: .9rem; text-transform: capitalize; }
.drow-ico  { width: 40px; height: 40px; }
.drow-desc { flex: 1; font-size: .82rem; color: rgba(255,255,255,.55); }
.drow-temps { display: flex; gap: 10px; align-items: baseline; }
.dmax { font-size: .95rem; font-weight: 600; }
.dmin { font-size: .85rem; color: rgba(255,255,255,.38); }
</style>
