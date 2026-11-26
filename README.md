# 3D Arena Action Prototype (Client Core)

Unity 3D 환경에서 클라이언트 개발 핵심 역량(물리 제어, 3D 수학, GC 최적화, 아키텍처 설계)을 단계별 스프린트로 구현·검증하는 프로젝트입니다.

## 개발 환경
* **Engine:** Unity 6 (6000.3.10f1)
* **Language:** C#
* **Version Control:** Git / GitHub (Feature Branch & PR Workflow)

---

## Sprint 진행 현황

- [x] **Sprint 1:** 플레이어 8방향 정속 이동 · 마우스 수평 조준 · 물리 제어
- [ ] **Sprint 2:** 적 AI 시야각(내적) 감지 · 좌우 판별(외적) · FSM 추적
- [ ] **Sprint 3:** 커스텀 오브젝트 풀링 & Profiler 기반 GC 최적화
- [ ] **Sprint 4:** 이벤트 기반 시스템 분리 & ScriptableObject 데이터 설계
- [ ] **Sprint 5:** C++ 코어 모듈 이식 & 최종 최적화 리포트

---

## 기술적 의사결정 및 트러블슈팅

### Sprint 1 — 플레이어 이동 및 조준 (`PlayerMove.cs`)
* **입력과 물리 루프 분리:** `Update()`에서 입력을 수집하고 `FixedUpdate()`에서 `Rigidbody.linearVelocity`와 `MoveRotation`을 적용해 고프레임/저프레임 환경의 입력 씹힘 및 중복 입력 방지.
* **Raycast 최적화 및 정확도 확보:** `Camera.main`을 `Awake()`에서 캐싱해 매 프레임 탐색 비용을 제거하고, `Ground` LayerMask 필터링으로 불필요한 충돌 검사 차단.
* **거리 연산 최적화:** 발밑 조준 데드존 판별 시 제곱근 연산을 생략하는 `sqrMagnitude` 사용.
* **Rigidbody 세팅 근거:**
  * `Interpolate`: 물리(50Hz)와 렌더링 프레임 차이로 발생하는 이동 떨림(Jitter) 완화.
  * `Collision Detection (Continuous)`: 빠른 이동 시 얇은 벽을 뚫는 터널링 방지.
  * `Freeze Rotation (X, Y, Z)`: 충돌 시 캡슐이 넘어지거나 회전력(Torque)으로 빙글빙글 도는 현상 차단.