import {ref, onMounted} from "vue"
import type {WeatherApiData} from '@/types/weatherapidata'

export function useWeatherApi() {
  const data = ref<WeatherApiData | null>(null);
  const loading = ref<boolean>(false);
  const error = ref<string | null>(null);

  const fetchWeatherApiData = async (): Promise<void> => {
    loading.value = true;
    error.value = null;
    try {
      const res = await fetch('/api/WeatherApi');
      if(!res.ok) throw new Error(`Сервер вернул код: ${res.status}`);
      data.value = await res.json() as WeatherApiData;
    } catch (ex) {
      error.value = ex instanceof Error ? ex.message : 'Не удалось загрузить данные';
    } finally {
      loading.value = false;
    }
  }

  onMounted(fetchWeatherApiData);
  return {data, error, loading, fetchWeatherApiData};
}
