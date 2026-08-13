import { useQuery } from "@tanstack/react-query";
import agent from "../api/agent";
import type { CurrentUserDto } from "../types/user";

export const useCurrentUser = () =>
  useQuery<CurrentUserDto>({
    queryKey: ["currentUser"],
    queryFn: () =>
      agent.get<CurrentUserDto>("/User/current").then((res) => res.data),
    staleTime: 5 * 60 * 1000, // 5 min
    retry: false,
  });
