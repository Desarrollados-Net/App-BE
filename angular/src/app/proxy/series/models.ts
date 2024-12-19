import type { EntityDto } from '@abp/ng.core';

export interface ReviewDto {
  calificacion: number;
  comentario?: string;
  fechaCreacion?: string;
  serieID: number;
  usuarioId?: string;
}

export interface EpisodioDto {
  numeroEpisodio: number;
  fechaEstreno?: string;
  titulo?: string;
  directores?: string;
  escritores?: string;
  duracion?: string;
  resumen?: string;
  temporadaID: number;
}

export interface SerieDto extends EntityDto<number> {
  titulo?: string;
  fechaLanzamiento?: string;
  duracion?: string;
  genero?: string;
  director?: string;
  escritor?: string;
  actores?: string;
  idioma?: string;
  paisOrigen?: string;
  fotoPortada?: string;
  calificacionIMDB?: string;
  imdbID?: string;
  totalTemporadas: number;
  temporadas: TemporadaDto[];
}

export interface TemporadaDto extends EntityDto<number> {
  titulo?: string;
  fechaLanzamiento?: string;
  numeroTemporada: number;
  serieID: number;
  episodios: EpisodioDto[];
}

export interface CreateUpdateSerieDto {
  titulo?: string;
  fechaLanzamiento?: string;
  duracion?: string;
  genero?: string;
  director?: string;
  escritor?: string;
  actores?: string;
  idioma?: string;
  paisOrigen?: string;
  fotoPortada?: string;
  calificacionIMDB?: string;
  imdbID?: string;
  totalTemporadas: number;
}



