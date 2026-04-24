import {ref, onMounted} from "vue"
import type {WeatherApiData} from '@/types/weatherapidata'

export function useWeatherApi() {
  const data = ref<WeatherApiData | null>(null);
  const loading = ref<boolean>(false);
  const error = ref<string | null>(null);

  const fetchWeatherApiData = async (mode: number = 0): Promise<void> => {
    loading.value = true;
    error.value = null;
    try {
      const res = await fetch(`/api/WeatherApi?mode=${mode}`);
      if (!res.ok) {
        const errorBody = await res.json();
        error.value = `Код ошибки: ${res.status} - ${errorBody.detail}`;
        return;
      }
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
